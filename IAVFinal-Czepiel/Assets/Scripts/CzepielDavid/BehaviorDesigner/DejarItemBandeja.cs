﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CzepielDavidProyectoFinal")]
[TaskDescription("Este task tiene como objetivo que los agentes vayan a la mesa donde se encuentra el pedido" +
    "en el que están trabajando para que puedan o llevarse el pedido o añadir algún item que hayan cocinado")]
public class DejarItemBandeja : Action
{
    //menu con el que estamos trabajando
    public SharedGameObject miMenu;

    //Lugar al que vamos a ir a por el meni
    public SharedGameObject miTarget;

    //Manager que controla las mesas en las que dejamos los pedidos
    public SharedGameObject mesasPedidos;

    public override TaskStatus OnUpdate()
    {
        miTarget.Value = mesasPedidos.Value.GetComponent<MesaColocarPedido>().getTableThatContainsThisOrder(miMenu.Value);
        return TaskStatus.Success;
    }
}