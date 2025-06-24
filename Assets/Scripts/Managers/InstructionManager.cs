using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    private int _instructionIndex=0;
    public GameObject generalInstructions;

    public void InstructionPlay(int val)
    {
        if (val == 2)
        {
            val = generalInstructions.transform.childCount - _instructionIndex;
        }
        if (val == 0)
        {
            PlayTheNextInstruction(_instructionIndex);
        }
        else
        {
            DisableTheCurrentInstruction(_instructionIndex);
            _instructionIndex += val;
            if (_instructionIndex >= generalInstructions.transform.childCount)
            {
                Time.timeScale = 1;
                GameManager.Instance.GeneralInstructions(false);
            }
            else
            {
                PlayTheNextInstruction(_instructionIndex);
            }
        }
    }

    private void PlayTheNextInstruction(int index)
    {
        generalInstructions.transform.GetChild(index).gameObject.SetActive(true);
    }

    private void DisableTheCurrentInstruction(int index)
    {
        generalInstructions.transform.GetChild(index).gameObject.SetActive(false);
    }
}
