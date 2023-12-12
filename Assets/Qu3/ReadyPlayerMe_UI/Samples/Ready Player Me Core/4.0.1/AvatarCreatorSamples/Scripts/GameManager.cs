using System;
using System.Collections.Generic;
using ReadyPlayerMe;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
    {
        [SerializeField] private AvatarCreatorStateMachine avatarCreatorStateMachine;
        [SerializeField] private AvatarConfig inGameConfig;
        private AvatarObjectLoader avatarObjectLoader;
        public GameObject LoadedAvatar;
        public GameObject FemaleAnimationPanel;
        public GameObject MaleAnimationPanel;
        [SerializeField] private Transform FemaleAnimationContent;
        [SerializeField] private Transform MaleAnimationContent;
        [SerializeField] internal List<RuntimeAnimatorController> FemaleAnimationList = new List<RuntimeAnimatorController>();
        [SerializeField] internal List<RuntimeAnimatorController> MaleAnimationList = new List<RuntimeAnimatorController>();
        private void OnEnable()
        {
            avatarCreatorStateMachine.AvatarSaved += OnAvatarSaved;
        }

        private void OnDisable()
        {
            avatarCreatorStateMachine.AvatarSaved -= OnAvatarSaved;
            avatarObjectLoader?.Cancel();
        }

        private void Start()
        {
            ChangeFemaleAnimation();
            ChangeMaleAnimation();
        }

        private void OnAvatarSaved(string avatarId)
        {
            Debug.Log(gameObject.name + " " + avatarId);
            
            
            avatarCreatorStateMachine.gameObject.SetActive(false);

            var startTime = Time.time;
            avatarObjectLoader = new AvatarObjectLoader();
            avatarObjectLoader.AvatarConfig = inGameConfig;
            avatarObjectLoader.OnCompleted += (sender, args) =>
            {
                AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, args.Avatar);
                LoadedAvatar = args.Avatar;
                if (args.Metadata.OutfitGender == OutfitGender.Feminine)
                {
                    FemaleAnimationPanel.SetActive(true);
                }
                else if (args.Metadata.OutfitGender == OutfitGender.Masculine)
                {
                    MaleAnimationPanel.SetActive(true);
                }
            };
            avatarObjectLoader.LoadAvatar(AvatarEndpoints.GetAvatarPublicUrl(avatarId));
        }
        
        
        internal void ChangeFemaleAnimation()
        {
            int i = 0;
            foreach (Transform child in FemaleAnimationContent)
            {
                int temp = i;
                child.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        LoadedAvatar.GetComponent<Animator>().runtimeAnimatorController = FemaleAnimationList[temp];
                    }
                );
                i++;
            }
            
            
        } 
        
        internal void ChangeMaleAnimation()
        {
            int i = 0;
            foreach (Transform child in MaleAnimationContent)
            {
                int temp = i;
                child.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        LoadedAvatar.GetComponent<Animator>().runtimeAnimatorController = MaleAnimationList[temp];
                    }
                );
                i++;
            }
            
            
        }
    }

