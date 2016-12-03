/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        private GameObject modelObj;
        public GameObject wh_placement, wh_construction1, wh_construction2, wh_final;
        public GameObject marker;
        private int next;
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
            InvokeRepeating("dispModel1", 1.0f, 0.0f);
            InvokeRepeating("dispModel2", 12.0f, 0.0f);
            InvokeRepeating("dispModel3", 23.0f, 0.0f);
            InvokeRepeating("dispModel4", 39.0f, 0.0f);

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }
        void dispModel1()
        {
            modelObj = (GameObject)Instantiate(wh_final, marker.transform.position, Quaternion.identity);
            // StartCoroutine(BlockWait());
            //print("start coroutine complete");
            Destroy(modelObj, 10.0f);
        }
        void dispModel2()
        {
            GameObject modelObj1 = (GameObject)Instantiate(wh_construction2, marker.transform.position, Quaternion.identity);
            Destroy(modelObj1, 10.0f);
        }
        void dispModel3()
        {
            GameObject modelObj2 = (GameObject)Instantiate(wh_construction1, marker.transform.position, Quaternion.identity);
            Destroy(modelObj2, 15.0f);

        }
        void dispModel4()
        {
            GameObject modelObj3 = (GameObject)Instantiate(wh_placement, marker.transform.position, Quaternion.identity);
            Destroy(modelObj3, 5.0f);
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
