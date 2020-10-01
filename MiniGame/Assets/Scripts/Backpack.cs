using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack
{
    public int[,] data;//用二维数组保存物品的存储信息(id)

    /*
    以每个物品的左上角坐标(x,y)为标志,
    向data写入物品时同步将其(x,y)写入其所占用的位置在storeData的映射(其x,y坐标)
    */
    public Vector2Int[,] storeData;

    public int newX = 0;
    public int newY = 0;


    //初始化, n,m表示最大长宽,而非可用长
    Backpack(int n, int m)
    {
        data = new int[n, m];
        storeData = new Vector2Int[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                data[i, j] = -1;
                storeData[i, j] = new Vector2Int(-1, -1);
            }
    }

    //单点物品放入检测函数, x,y为受检测点的坐标
    private bool Checkxy(int x, int y, Item item)
    {
        for (int i = 0; i < item.height; i++)
            for (int j = 0; j < item.width; j++)
                if (data[x + i, y + j] != 0)
                    return false;
        return true;
    }

    //物品放入的执行函数, 配合单点检测进行暴力扫描
    public Vector2Int PutIn(Item item)
    {
        bool flag = false;
        Vector2Int t = new Vector2Int(-1, -1);//(-1,-1)表示不可用,倘能放入物品, 则为物品的左上角坐标

        for (int i = 0; i < data.GetLength(0) && !flag; i++)//从行扫描
        {
            for (int j = 0; j < data.GetLength(1); j++)//从列扫描
            {
                if (Checkxy(i, j, item))//逐点判断
                {
                    t = new Vector2Int(i, j);
                    flag = true;
                    break;
                }
            }
        }

        if (!flag)
        {
            return t;//然后应该直接播放Drop的动画
        }

        for (int i = 0; i < item.height; i++)
        {
            for (int j = 0; j < item.width; j++)
            {
                data[i + t.x, j + t.y] = item.id;
                storeData[i + t.x, j + t.y] = t;
            }
        }

        return t;
    }

    //单点物品减少执行函数, x,y为受执行点的坐标
    public void itemReduction(int x, int y, Item item)
    {
        if (storeData[x, y].x < 0)
            return;

        Vector2Int t = new Vector2Int(-1, -1);

        //记录物品原先的存储位置
        int itemX = storeData[x, y].x;
        int itemY = storeData[x, y].y;

        //从data和storeData层面删除物品
        for (int i = 0; i < item.height; i++)
        {
            for (int j = 0; j < item.width; j++)
            {
                data[i + itemX, j + itemY] = 0;//不能在这里置为-1, cuz大件物品占有的格子未必全被删除
                storeData[i + itemX, j + itemY] = t;
            }
        }
        //UI动画();
    }

    //单点格子增加函数
    //(暂未考虑越界!!!!)
    public void GridGeneration()
    {
        if (newX > newY)//行内
        {
            data[newX, newY] = 0;
            newY++;
        }
        else if (newX == newY)//行的末端
        {
            data[newX, newY] = 0;
            newX = 0;
            newY++;
        }
        else if (newX < newY - 1)//列内
        {
            data[newX, newY] = 0;
            newX++;
        }
        else if (newX == newY - 1)//列的末端
        {
            data[newX, newY] = 0;
            newX = newY;
            newY = 0;
        }
    }

    //单点格子减少函数
    public void GridReduction()
    {
        if (newX == 0 && newY == 0)//仅剩一格
        {
            data[newX, newY] = -1;
            //GameOver();
            return;
        }
        if (newX + 1 > newY && newY != 0)//行内
        {
            data[newX, newY] = -1;
            newY--;
        }
        else if (newY == 0)//行末
        {
            data[newX, newY] = -1;
            newY = newX;
            newX--;
        }
        else if (newX < newY && newX != 0)//列内
        {
            data[newX, newY] = -1;
            newX--;
        }
        else if (newX == 0)//列末
        {
            data[newX, newY] = -1;
            newY--;
            newX = newY;
        }
    }


}
