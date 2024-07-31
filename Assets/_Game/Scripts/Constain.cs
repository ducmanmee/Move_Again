using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constain
{
    //Timmer
    public static float TIMER_DEAD = 1.5f;
    public static float TIMER_WIN = 3f;

    //Animation
    public static string ANIM_IDLE = "idle";
    public static string ANIM_RUN = "run";
    public static string ANIM_ATTACK = "attack";
    public static string ANIM_ULTI = "ulti";
    public static string ANIM_DANCEWIN = "dancewin";
    public static string ANIM_DANCE = "danceshop";
    public static string ANIM_DEAD = "dead";


    //Game Tag
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_TARGET = "target";
    public static string TAG_TABKILL = "tabkill";
    public static string TAG_PLAYER = "Player";
    public static string TAG_ATTACKRANGE = "attackRange";
    public static string TAG_WEAPON = "weapon";

    public static string SOUND_ATTACK = "PAttack";
    public static Vector3 START_POINT = Vector3.zero;
    public static Vector3 ROTATION_WIN = new Vector3(0f, 180f, 0f);

}
