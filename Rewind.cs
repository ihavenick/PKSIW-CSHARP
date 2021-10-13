using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using UnrealEngine.Framework;

namespace PKSIW
{
    public class Rewind : ISystem
    {
        public struct RewindParams
        {
            public Vector3 Velocity;
            public bool isInAir;
            public bool isDead;
            public bool isEquiped;
            public float Healt;
            public Transform Transform;
            public AnimationAsset Anim;
            public AnimationMontage AnimMontage;
        }
        
        public bool IsRewinding;

        public static List<RewindParams> RewindArray;
        
        private SceneComponent CapsuleComponent;
        
      //  private Character Test;

        public Rewind()
        {
            RewindArray = new List<RewindParams>
            {
                Capacity = 700
            };
           // Test = World.GetFirstPlayerController().GetCharacter();
           

        }
        
       

        public static void TestRewind(ObjectReference self)
        {
            if(RewindArray.Capacity != 600)
                RewindArray.Capacity = 600;



           // Character Test = World.GetFirstPlayerController().GetCharacter();
            Character Test = self.ToActor<Character>();
            //CapsuleComponent gelen = self.ToComponent<CapsuleComponent>();
            CapsuleComponent gelen = Test.GetComponent<CapsuleComponent>();
            SkeletalMeshComponent skeletalMeshComponent = Test.GetComponent<SkeletalMeshComponent>();
            
            bool IsRewinding = false;
            Test.GetBool("IsRewinding",ref IsRewinding);

            AnimationInstance ai = skeletalMeshComponent.GetAnimationInstance(); 
            
            if (IsRewinding)
            {
                
                RewindParams songelen = RewindArray.LastOrDefault();
                
                
                Vector3 a = Vector3.Zero;
                
                if(songelen.Transform.Location!= a)
                    gelen.SetWorldTransform(songelen.Transform);

                if (songelen.AnimMontage!=null)
                {
                    ai.PlayMontage(songelen.AnimMontage, -1f, 999f, true);
                }
               
                Test.GetComponent<RotatingMovementComponent>("CharacterMovement").SetVelocity(songelen.Velocity);
                //gelen.SetPhysicsLinearVelocity(songelen2.Location,false,null);

                if (RewindArray.Count()>0)
                {
                    RewindArray.Remove(songelen);
                }
                
                
            }
            else
            {
                if (RewindArray.Capacity == RewindArray.Count)
                    RewindArray.Remove(RewindArray.FirstOrDefault());
                RewindParams rp = new RewindParams();
                rp.Velocity = Test.GetComponent<RotatingMovementComponent>().GetVelocity();
                rp.Transform = gelen.GetTransform();
                
                if ( ai.GetCurrentActiveMontage()!=null)
                {
                    rp.AnimMontage = ai.GetCurrentActiveMontage();
                }
                
                //Debug.AddOnScreenMessage(-1, 30.0f, Color.LimeGreen, " Normal zaman " + gelen.GetTransform().ToString());
                RewindArray.Add(rp);
            }
            
        }


        public void OnEndPlay()
        {
            Debug.ClearOnScreenMessages();
        }
    }
}