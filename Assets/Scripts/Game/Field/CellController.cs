using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellController
{
    /*public void Deselection(Transform objs, Material baseMat) {
        GameObject cell = FindCell(objs);
        if (cell) {
            switch (cell.tag)
            {
                case "Selected":
                    cell.GetComponent<MeshRenderer>().material = baseMat;
                    break;
                case "SelectedChicken":
                    cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = baseMat;
                    break;                
            }            
            cell.tag = "Untagged";
        }        
    }

    public GameObject FindCell(Transform cells)
    {           
        for (int i = 0; i < cells.childCount; i++)
        {
            if (cells.GetChild(i).tag == "Selected" || cells.GetChild(i).tag == "SelectedChicken")
            {

                return cells.GetChild(i).gameObject;
            }            
        }

        return null;
    }

    public void DesroyCell()
    {
        Transform currentField = LevelManager.currentField.transform;
        for (int i = 0; i < currentField.childCount; i++)
        {
            if (currentField.GetChild(i).tag == "Selected")
            {
                currentField.GetChild(i).tag = "Untagged";
                currentField.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
    }

    public void MoveChicken(Transform cells, Transform chickens)
    {
        GameObject cell = FindCell(cells);
        GameObject chicken = FindCell(chickens);
        if (cell != null && chicken != null)
        {
            chicken.transform.DOMove(new Vector3(cell.transform.position.x, chicken.transform.position.y, cell.transform.position.z), 1);
        }
        
    }*/
}
