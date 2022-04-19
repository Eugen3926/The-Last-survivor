using UnityEngine;
using System.Collections.Generic;

public class CreateLevel
{

    public void CreateField(Transform field, Vector2 fieldSize, string cellType)
    {
        float offsetX = 0; // Cell offset along the axis X
        float indentX = 1.74f; // Indent between cells along the axis X
        float indentY = 1.51f; // Indent between cells along the axis Y

        int p = 0;
        Transform cell;

        switch (cellType) {
            case "Polygon":
                offsetX = 0.435f;
                break;
            case "Cube":
                offsetX = 0;
                indentX = indentY = 1f;                
                break;
        }
        
        
        for (int i = 1; i <= fieldSize.x; i++)
        {
            for (int j = 1; j <= fieldSize.y; j++)
            {
                cell = field.GetChild(p); 
                cell.position = new Vector3(offsetX + (j * cell.localScale.x * indentX), 0f, i * cell.localScale.x * indentY);
                cell.name = "Cell " + i + "_" + j;
                p++;
            }
            offsetX *= -1;
        }
        //CreateWall(field);
    }

    private void CreateWall(Transform field)
    {
        int wallCount = Random.Range(8,16);
        
        for (int i = 0; i < wallCount; i++)
        {
            int wallLength = Random.Range(3, 7);
            Transform cell = FindCell(field, "emptyCell");       
            
            cell.localPosition = new Vector3(cell.localPosition.x, 1f, cell.localPosition.z);
            cell.tag = "Wall";
            Transform currentCell = cell;
            for (int j = 1; j < wallLength; j++)
            {
                List<Transform> cells = FindNeighbor(currentCell, field);                
                
                if (cells.Count > 0)
                {
                    Transform cellNeighbor = null;
                    for (int p = 0; p < 100; p++)
                    {
                        cellNeighbor = cells[Random.Range(0, cells.Count)];
                        if (cellNeighbor.tag == "emptyCell") p=100;                        
                    }              

                    cellNeighbor.localPosition = new Vector3(cellNeighbor.localPosition.x, 1f, cellNeighbor.localPosition.z);
                    cellNeighbor.tag = "Wall";
                    currentCell = cellNeighbor;
                }
                else Debug.Log(currentCell.name + " doesn't have any Neighbor");
                
            }            
        }        
    }

    public List<Transform> FindNeighbor(Transform cell, Transform field) {
        string str = cell.name.Trim(new char[] { 'C', 'e', 'l', ' ' });
        string[] coordinates = str.Split(new char[] { '_' });
        int posX = int.Parse(coordinates[0]);
        int posY = int.Parse(coordinates[1]);
        List<Transform> cells = new List<Transform>();
        int[] mass = new int[] { 1, -1, 1, 0, 0, 1, -1, 0, -1, -1, 0, -1 };

        for (int i = 0; i < mass.Length; i++)
        {
            string neighborName = "Cell " + (posX + mass[i]) + "_" + (posY + mass[i+1]);
            Transform cellNeighbor = field.Find(neighborName);
            if (cellNeighbor != null)
            {
                cells.Add(cellNeighbor);
            }
            i++;
        }
        
        return cells;
    }

    private Transform FindCell(Transform field, string cellTag)
    {
        Transform cell;
        do
        {
            cell = field.GetChild(Random.Range(0, field.childCount)).transform;
            
        } while (cell.tag != cellTag);
        
        return cell;
    }




    public List<Transform> GetEmptyCells(Transform field)
    {
        List<Transform> cells = new List<Transform>();

        for (int i = 0; i < field.childCount; i++)
        {
            if (field.GetChild(i).tag == "emptyCell")
            {
                cells.Add(field.GetChild(i));
            }
        }

        return cells;
    }
    
    public void CreateCollapse(Transform collapses, List<Transform> emptyCells)
    {
        GameObject collapse = null;
        bool isUnderCharacter = false;

        /*for (int i = 0; i < collapses.childCount; i++)
        {
            if (collapses.GetChild(i).gameObject.activeSelf && collapses.GetChild(i).transform.position.x == LevelController.currentCell.position.x && collapses.GetChild(i).transform.position.z == LevelController.currentCell.position.z)
            {
                isUnderCharacter = true;
            }

            if (!collapses.GetChild(i).gameObject.activeSelf)
            {
                collapse = collapses.GetChild(i).gameObject;
            }
        }*/
        CollapseInitialization(collapse, emptyCells, isUnderCharacter);
    }

    private void CollapseInitialization(GameObject collapse, List<Transform> emptyCells, bool isUnderCharacter)
    {
        if (collapse != null)
        {
            // Collapse under player

            /*if (!isUnderCharacter)
            {
                collapse.transform.position = new Vector3(Player.currentCell.position.x, collapse.transform.position.y, Player.currentCell.position.z);
            }
            else
            {
                Transform randCell = emptyCells[Random.Range(0, emptyCells.Count)];
                collapse.transform.position = new Vector3(randCell.position.x, collapse.transform.position.y, randCell.position.z);
            }*/

            // Collapse in the random cell
            Transform randCell = emptyCells[Random.Range(0, emptyCells.Count)];
            collapse.transform.position = new Vector3(randCell.position.x, collapse.transform.position.y, randCell.position.z);
            collapse.SetActive(true);
        }

    }

    public void CreateBonus(Transform bonus, List<Transform> emptyCells)
    {
        Transform cell = emptyCells[Random.Range(0, emptyCells.Count)];
        cell.tag = "notEmptyCell";
        bonus.position = new Vector3(cell.position.x, bonus.position.y, cell.position.z);
        
    }
}
