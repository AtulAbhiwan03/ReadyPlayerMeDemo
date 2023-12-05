using ReadyPlayerMe.Core;
using UnityEngine;

namespace ReadyPlayerMe.Samples
{
    /// <summary>
    /// This class is a simple <see cref="Monobehaviour"/>  to serve as an example on how to load Ready Player Me avatars and spawn as a <see cref="GameObject"/> into the scene.
    /// </summary>
    public class AvatarLoadingExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Set this to the URL or shortcode of the Ready Player Me Avatar you want to load.")]
        private string avatarUrl = "https://api.readyplayer.me/v1/avatars/638df693d72bffc6fa17943c.glb";

        private GameObject avatar;
        // Add 2 editable fields to store the new masculine and feminine animator controllers
        [SerializeField] private RuntimeAnimatorController masculineController;
        [SerializeField] private RuntimeAnimatorController feminineController;

        private void Start()
        {
            ApplicationData.Log();
            var avatarLoader = new AvatarObjectLoader();
            // use the OnCompleted event to set the avatar and setup animator
            avatarLoader.OnCompleted += (_, args) =>
            {
                avatar = args.Avatar;
                SetAnimatorController(args.Metadata.OutfitGender); //  <--------------- ADDED
            };
            avatarLoader.LoadAvatar(avatarUrl);
        }
		
        // This method is used to reassign the appropriate animator controller based on outfit gender
        private void SetAnimatorController(OutfitGender outfitGender)
        {
            var animator = avatar.GetComponent<Animator>();
            // set the correct animator based on outfit gender
            if (animator != null && outfitGender == OutfitGender.Masculine)
            {
                animator.runtimeAnimatorController = masculineController;
            }
            else
            {
                animator.runtimeAnimatorController = feminineController;
            }
        }

        private void OnDestroy()
        {
            if (avatar != null) Destroy(avatar);
        }
    }
}
