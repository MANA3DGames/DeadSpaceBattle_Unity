﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MANA3D.UI.Tween
{
    public class UITweenVector3 : UITween
    {
        protected Vector3Values xVal;
        protected Vector3Values yVal;
        protected Vector3Values zVal;

        protected Vector3 fromVector3;

        public UITweenVector3( string name, Type type,
                               Vector3 vector3, Vector3 from, Vector3 to,
                               float duration = 1, float delay = 0, 
                               System.Action onComplete = null,
                               UIMenu menu = null, UITweenManager manager = null,
                               bool removeOnComplete = true,
                               Action startAction = Action.None, Action endAction = Action.None ) 
                               : base( name, menu, manager, startAction, endAction, removeOnComplete )
        {
            this.duration = duration;
            this.delay = delay;

            xVal = new Vector3Values();
            yVal = new Vector3Values();
            zVal = new Vector3Values();

            if ( type == Type.Absolute ) 
            {
                xVal.to = to.x;
                xVal.min = Mathf.Min( from.x, to.x );
                xVal.max = Mathf.Max( from.x, to.x );
                xVal.interval = ( to.x - from.x ) / ( duration * 60 );
                //xVal.factor = (int)( xVal.interval / Mathf.Abs( xVal.interval ) );

                yVal.to = to.y;
                yVal.min = Mathf.Min( from.y, to.y );
                yVal.max = Mathf.Max( from.y, to.y );
                yVal.interval = ( to.y - from.y ) / ( duration * 60 );
                //yVal.factor = (int)( yVal.interval / Mathf.Abs( yVal.interval ) );

                zVal.to = to.z;
                zVal.min = Mathf.Min( from.z, to.z );
                zVal.max = Mathf.Max( from.z, to.z );
                zVal.interval = ( to.z - from.z ) / ( duration * 60 );
                //zVal.factor = (int)( zVal.interval / Mathf.Abs( zVal.interval ) );

                fromVector3 = from;
            }
            else if ( type == Type.Relative )
            {
                Vector3 oldFrom = vector3;
                Vector3 newFrom = vector3 + from;
                Vector3 newTo = new Vector3( oldFrom.x + to.x, oldFrom.y + to.y, oldFrom.z + to.z );

                xVal.to = newTo.x;
                xVal.min = Mathf.Min( newFrom.x, newTo.x );
                xVal.max = Mathf.Max( newFrom.x, newTo.x );
                xVal.interval = ( newTo.x - newFrom.x ) / ( duration * 60 );
                //xVal.factor = (int)( xVal.interval / Mathf.Abs( xVal.interval ) );

                yVal.to = newTo.y;
                yVal.min = Mathf.Min( newFrom.y, newTo.y );
                yVal.max = Mathf.Max( newFrom.y, newTo.y );
                yVal.interval = ( newTo.y - newFrom.y ) / ( duration * 60 );
                //yVal.factor = (int)( yVal.interval / Mathf.Abs( yVal.interval ) );
                //yVal.factor = (int)( yVal.interval / yVal.interval );

                zVal.to = newTo.z;
                zVal.min = Mathf.Min( newFrom.z, newTo.z );
                zVal.max = Mathf.Max( newFrom.z, newTo.z );
                zVal.interval = ( newTo.z - newFrom.z ) / ( duration * 60 );
                //zVal.factor = (int)( zVal.interval / Mathf.Abs( zVal.interval ) );

                fromVector3 = newFrom;
            }

            int noneZeroCount = 0;
            if ( xVal.interval != 0 ) 
                noneZeroCount++;
            if ( yVal.interval != 0 ) 
                noneZeroCount++;
            if ( zVal.interval != 0 ) 
                noneZeroCount++;

            if ( noneZeroCount == 0 )
                noneZeroCount = 1;

            interval = ( xVal.interval + yVal.interval + zVal.interval ) / noneZeroCount;

            this.onComplete = onComplete;
        }
    }
}

