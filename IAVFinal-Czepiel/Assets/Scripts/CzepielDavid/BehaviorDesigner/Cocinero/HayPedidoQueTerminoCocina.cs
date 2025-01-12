﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CzepielDavidProyectoFinal/Cocinero")]
[TaskDescription("Este condición tiene como objetivo preguntar si hay pedidos en la cocina que ya no necesiten ningún elemento cocinable \n" +
    "en caso afirmativo el cocinero se lo lleva para darselo a los cajeros y que lo completen")]
public class HayPedidoQueTerminoCocina : Conditional
{
    //Cocina manager al que le voy a preguntar por los pedidos que me interesan
    public SharedGameObject cocinaManager;

    private CocinaManager cocina;

    //Variable que va a almacenar el posible pedido que haya terminado de completar su parte de la cocina
    public SharedGameObject pedido;

    public override void OnStart()
    {
        cocina = cocinaManager.Value.GetComponent<CocinaManager>();
    }

    public override TaskStatus OnUpdate()
    {
        //Le pido un pedido de estos, si me da algo me lo quedo y lo saco de la cocina
        pedido.Value = cocina.getOrderWithNoKitchenItemsLeftToComplete();
        if (pedido.Value != null)
        {
            cocina.removeOrderFromInCompletionList(pedido.Value);
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Failure;
    }
}