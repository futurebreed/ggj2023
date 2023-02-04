using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    [SerializeField]
    private InputDragBehavior inputBehavior;

    private LineRenderer line;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float ropeSegLen = 0.25f;
    public int segmentLength = 35;
    [Range(0,.9f)] public float elasticity = 0.5f;

    public Vector2 tension;
    
    public struct RopeSegment {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos) {
            posNow = pos;
            posOld = pos;
        }
    }

    public void DrawRope(){
        Vector3[] ropePositions = new Vector3[segmentLength];
        int i;

        for(i=0; i < segmentLength; i++){
            ropePositions[i] = ropeSegments[i].posNow;
        }

        line.startWidth = .65f;
        line.endWidth = .02f;
        line.SetPositions(ropePositions);
        line.positionCount = ropePositions.Length;
        
    }

    public void Awake() {
        line = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,4f));

        for(int i=0;i<segmentLength;i++){
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    public void Update(){
        DrawRope();
    }

    public void FixedUpdate() {
        Simulate();
    }

    public void Simulate(){

        for(int i=1;i<segmentLength;i++){
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += tension * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;
        }

        for(int i=0;i<50;i++){
            Physics();
        }
    }

    public void Physics(){
        RopeSegment zero = ropeSegments[0];
        zero.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,4f));
        ropeSegments[0] = zero;

        for(int i=0;i<segmentLength-1;i++){
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i+1];
            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if(dist > ropeSegLen) {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if(dist < ropeSegLen) {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if(i !=0) {
                firstSeg.posNow -= changeAmount * elasticity;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * elasticity;
                ropeSegments[i+1] = secondSeg;
            } else {
                secondSeg.posNow += changeAmount;
                ropeSegments[i+1] = secondSeg;
            }
        }
    }
}
