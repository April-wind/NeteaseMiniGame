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

    //当前可用的最新的格子的坐标
    public int newX = 0;
    public int newY = 0;


    //初始化, n表示最大长宽(初始化成方阵),而非可用长
    public Backpack(int n)
    {
        data = new int[n, n];
        storeData = new Vector2Int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                data[i, j] = -1;
                storeData[i, j] = new Vector2Int(-1, -1);
            }
        data[0, 0] = 0;
        for (int i = 0; i < 8; i++)
        {
            GridGeneration();
        }
    }

    //单点物品放入检测函数, x,y为受检测点的坐标
    private bool CheckPut(int x, int y, Item item)
    {
        //越界检测
        if (x + item.height > data.GetLength(0))
            return false;
        if (y + item.width > data.GetLength(1))
            return false;

        for (int i = 0; i < item.height; i++)
            for (int j = 0; j < item.width; j++)
                if (data[x + i, y + j] != 0)
                    return false;
        return true;
    }

    public bool PointPutIn(int x, int y, Item item)
    {
        if (CheckPut(x, y, item))
        {
            for (int i = 0; i < item.height; i++)
            {
                Vector2Int t = new Vector2Int(x, y);
                for (int j = 0; j < item.width; j++)
                {
                    data[i + t.x, j + t.y] = item.id;
                    storeData[i + t.x, j + t.y] = t;
                }
            }
            return true;
        }
        return false;
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
                if (CheckPut(i, j, item))//逐点判断
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
    public void ItemReduction(int x, int y)
    {

        if (storeData[x, y].x < 0)
            return;

        //记录物品原先的存储位置
        int itemX = storeData[x, y].x;
        int itemY = storeData[x, y].y;
        Item item = BackpackManager.instance.myInventory.itemList[data[itemX, itemY]];
        Vector2Int t = new Vector2Int(-1, -1);

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

    //单点格子增加函数(已考虑越界,即扩充至data[n,n]时)
    public void GridGeneration()
    {
        if (newX > newY)//行内
        {
            newY++;
            data[newX, newY] = 0;
        }
        else if (newX == newY)//行的末端
        {
            if (newX == data.GetLength(0) - 1)//越界检测
                return;
            newX = 0;
            newY++;
            data[newX, newY] = 0;
        }
        else if (newX < newY - 1)//列内
        {
            newX++;
            data[newX, newY] = 0;
        }
        else if (newX == newY - 1)//列的末端
        {
            newX = newY;
            newY = 0;
            data[newX, newY] = 0;
        }
    }

    //单点格子减少函数(已考虑越界,即减少至[0,0]时,但可能导致增加函数出bug)
    public int GridReduction()
    {
        int id = 0;
        if (data[newX, newY] != 0)
        {
            id = data[newX, newY];
            ItemReduction(newX, newY);
        }

        if (newY == 0)//行末
        {
            data[newX, newY] = -1;
            if (newX == 0)//越界检测
            {
                //GameOver();
                //此时游戏应该结束, 否则继续运行GridGeneration()将出现忽略[0,0]的错误
                return 0;
            }
            newY = newX;
            newX--;
        }
        else if (newX == 0)//列末
        {
            data[newX, newY] = -1;
            newY--;
            newX = newY;
        }
        else if (newX + 1 > newY)//行内
        {
            data[newX, newY] = -1;
            newY--;
        }
        else if (newX < newY)//列内
        {
            data[newX, newY] = -1;
            newX--;
        }
        return id;
    }


}
