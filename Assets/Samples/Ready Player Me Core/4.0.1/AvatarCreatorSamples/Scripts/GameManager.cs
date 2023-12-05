using System.Collections.Generic;
using ReadyPlayerMe;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using UnityEngine;
     public class GameManager : MonoBehaviour
    {
        [SerializeField] private AvatarCreatorStateMachine avatarCreatorStateMachine;
        [SerializeField] private AvatarConfig inGameConfig;
        public List<RuntimeAnimatorController> animationclip = new List<RuntimeAnimatorController>();
        private AvatarObjectLoader avatarObjectLoader;

        private void OnEnable()
        {
            avatarCreatorStateMachine.AvatarSaved += OnAvatarSaved;
        }

        private void OnDisable()
        {
            avatarCreatorStateMachine.AvatarSaved -= OnAvatarSaved;
            avatarObjectLoader?.Cancel();
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
                args.Avatar.GetComponent<Animator>().runtimeAnimatorController = animationclip[0];
                //DebugPanel.AddLogWithDuration("Created avatar loaded", Time.time - startTime);
            };

            avatarObjectLoader.LoadAvatar(AvatarEndpoints.GetAvatarPublicUrl(avatarId));
        }
    }

