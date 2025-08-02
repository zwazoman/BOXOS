using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class ArmInputHandler : MonoBehaviour
{
    [SerializeField] Arm _arm;

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }

    Vector2 stickInput;

    [HideInInspector] public List<ArmInput> armInputs = new List<ArmInput>();

    // Update is called once per frame
    void Update()
    {
        if (armInputs.Count == 0)
            return;

        stickInput = _arm.player.GetStickVector(_arm.side);

        for (int i = 0; i < armInputs.Count; i++)
        {
            ArmInput armInput = armInputs[i];

            Vector2 currentTargetDirection = armInput.directions[armInput.directionCpt];

            if (InputTools.InputAngle(currentTargetDirection, stickInput))
            {
                print(armInput.directionCpt);
                print(armInput.directions.Count);
                if (armInput.directionCpt == armInput.directions.Count - 1)
                {
                    float inputTime = 0;

                    switch (armInput.directionCpt)
                    {
                        case 0:
                            inputTime = .1f;
                            break;
                        case 1:
                            inputTime = .04f;
                            break;
                        case 2:
                            inputTime = 0f;
                            break;
                    }

                    print("ALLO");
                    armInput.endingTimer += Time.deltaTime;

                    if (armInput.endingTimer >= inputTime)
                        armInput.Perform();
                }
                else
                    armInput.directionCpt++;
            }
            else
                armInput.endingTimer = 0;
        }
    }

    public void ClearArmInputs()
    {
        foreach (ArmInput armInput in armInputs)
            armInput.Reset();

        armInputs.Clear();
    }
}
