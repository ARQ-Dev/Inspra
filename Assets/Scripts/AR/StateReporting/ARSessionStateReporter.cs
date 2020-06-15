using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQ.AR.StateReporter
{
    interface IARSessionStateReporter
    {
        void OnPlaneDetected(bool isPlaneDetected);

        void None();

        void Unsupported();

        void CheckingAvalibility();

        void NeedsInstall();

        void Installing();

        void Ready();

        void SessionInitialized();

        void SessionTracking();

    }
}


