﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CzepielDavidProyectoFinal/Cliente")]
[TaskDescription("Este task tiene como objetivo liberar uno de los lugares que alguno de los clientes haya estado ocupando ")]
public class LiberarLugar : Action
{
    //Lugar que he estado ocupando y que voy a liberra
    public SharedGameObject miTarget;

    //manager al que le voy a indicar que he liberado el lugar
    public SharedGameObject lugaresManager;

    public override TaskStatus OnUpdate()
    {
        lugaresManager.Value.GetComponent<LugaresDesgastablesManager>().leavePlace(miTarget.Value);
        return TaskStatus.Success;
    }
}