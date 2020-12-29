using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            // Dont do anything if the currentAction is the same
            if (currentAction == action) return;

            // Cancel the previous action
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            // Set the new action
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }

}