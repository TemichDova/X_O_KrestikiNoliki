using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGame {
    
    //размер поля
    private int sizeField;

    private float posY, posX;
    // размер сетки
    private float sizeGrid;
    //номер клетки
    private int indexPoint;
    private Vector2[] pointGrid;

    // режим игры
    // 0 - player vs PC
    // 1 - player vs player
    // 2 - PC vs PC
    private int gameMode;
    
    //Номер игрока/бота
    private int numberPlayr;

    public SettingGame() { }

     public SettingGame(int _sizeField, int _gameMode)
     {
        sizeField = _sizeField;
        gameMode = _gameMode;
     
     }


    public void DefoltGame()
    {
        sizeField = 3;
        gameMode = 0;

        
    }

    public void GridGeneration()
    {
        pointGrid = new Vector2[(sizeField) * (sizeField)];

        for (int i = 0, y = -sizeField/2; y <= sizeField/2;  y++)
        {
            for (int x = -sizeField/2; x<=sizeField/2; x++, i++)
            {

                pointGrid[i] = new Vector2(x*60,y*60);
            }
        }
        
        //Просто оставлю, Генерация сетки против часовой стрелки , отсчет с центра в 4 направлениях
        /*
        pointGrid[0] = new Vector2(0, 0);

        for (int i=0, x = 1, y = 1, radius = sizeField/2, countLeftSide = 1,countLastSide =0, indexPoint= 1; i< radius; i++, countLeftSide++, countLastSide++, x++, y++)
        {


            pointGrid[indexPoint] = new Vector2(0, (y) * 60);
            pointGrid[indexPoint + 1] = new Vector2((-x) * 60, 0);
            pointGrid[indexPoint + 2] = new Vector2(0, (-y) * 60);
            pointGrid[indexPoint + 3] = new Vector2((x) * 60, 0);
            indexPoint += 4;
            for (int left_side=0;left_side<countLeftSide; left_side++)
                {
                pointGrid[indexPoint] = new Vector2((-x+ left_side) * 60, (y) * 60);
                pointGrid[indexPoint+1] = new Vector2((-x) * 60, (-y+ left_side) * 60);
                pointGrid[indexPoint+2] = new Vector2((x- left_side) * 60, (-y) * 60);
                pointGrid[indexPoint+3] = new Vector2((x) * 60, (y- left_side) * 60);
                indexPoint += 4;
     
                }
            for (int n = 1; n <= countLastSide; n++)
            {
                pointGrid[indexPoint] = new Vector2((-x) * 60, (y - n) * 60);
                pointGrid[indexPoint + 1] = new Vector2((-x + n) * 60, (-y) * 60);
                pointGrid[indexPoint + 2] = new Vector2((x) * 60, (-y + n) * 60);
                pointGrid[indexPoint + 3] = new Vector2((x - n) * 60, (y) * 60);
                indexPoint += 4;
            }
            
        }*/

    }

    public int GetSizeViled
    {
        get
        {
            return sizeField;
        }
    }

    public Vector2[] GetPointGrid
    {
        
        get
        {
          return pointGrid;
        }
        
        
    }
    
    public int GetGameMode
    {
        get { return gameMode; }
        
    }
    

    public void ShowSetting()
    {
        Debug.Log(sizeField);
        Debug.Log(gameMode);

    }
}
