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
        public List<RuntimeAnimatorController> animationclip = new List<RuntimeAnimatorController>();
        private AvatarObjectLoader avatarObjectLoader;
        public GameObject LoadedAvatar;
        public GameObject FemaleAnimationPanel;
        [SerializeField] private Transform animationContent;
        [SerializeField] internal List<RuntimeAnimatorController> FemaleAnimationList = new List<RuntimeAnimatorController>();
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
                /*args.Avatar.GetComponent<Animator>().runtimeAnimatorController = animationclip[0];*/
                LoadedAvatar = args.Avatar;
                if (args.Metadata.OutfitGender == OutfitGender.Feminine)
                {
                    FemaleAnimationPanel.SetActive(true);
                }
                else
                {
                    Debug.Log("Male is selected");
                }
                //DebugPanel.AddLogWithDuration("Created avatar loaded", Time.time - startTime);
            };

            avatarObjectLoader.LoadAvatar(AvatarEndpoints.GetAvatarPublicUrl(avatarId));
        }
        
        
        internal void ChangeFemaleAnimation()
        {
            int i = 0;
            foreach (Transform child in animationContent)
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
    }

