using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class ArmInputsHandler : MonoBehaviour
{
    [SerializeField] Arm _arm;

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }

    Vector2 stickInput;

    [HideInInspector] public List<ActionData> actionDatas = new();

    // Update is called once per frame
    void Update()
    {
        if (actionDatas.Count == 0)
            return;

        stickInput = _arm.player.GetStickVector(_arm.side);

        for (int i = 0; i < actionDatas.Count; i++)
        {
            ArmInput armInput = actionDatas[i].inputs;

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
                        armInput.Perform(actionDatas[i].type);
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
        foreach (ActionData actionData in actionDatas)
            actionData.inputs.Reset();

        actionDatas.Clear();
    }
}
