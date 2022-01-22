using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class Player : IComparable<Player>
{
    public int id; //no s'e si lo lleguemos a usar porque en realidad lo buscamos por su Index
    public string name; //Nombre del jugador
    public int seat; //En que silla esta sentado

    public int gamePoints; //Puntos totales del Game
    public bool isLeader; //No se si esto viva aqui porque existe ya en el Match Manager
    public float matchScore; //Su score calculado actual

    public Player(int newID, string newName, int newSeat) //La clase Player con sus variables
    {
        id = newID;
        name = newName;
        seat = newSeat;
    }

    //This method is required by the IComparable interface. 
    public int CompareTo(Player other) //Esto no estoy muy seguro para que es pero debe existir
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return id - other.id;
    }
}