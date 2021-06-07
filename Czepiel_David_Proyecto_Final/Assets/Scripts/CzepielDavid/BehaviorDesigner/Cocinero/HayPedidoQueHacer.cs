﻿namespace UCM.IAV.Movimiento
{
    using System.Collections;
    using System.Collections.Generic;
    using Bolt;
    using Ludiq;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
    using UnityEngine.AI;

    [TaskCategory("CzepielDavidProyectoFinal/Cocinero")]
    [TaskDescription("Rellenar")]
    public class HayPedidoQueHacer : Conditional
    {
        public SharedGameObject cajaManager;
        public SharedGameObject pedido;
        public SharedGameObject cocinaManager;
        private CajaManager caja;
        private CocinaManager cocina;

        public override void OnStart()
        {
            caja = cajaManager.Value.GetComponent<CajaManager>();
            cocina = cocinaManager.Value.GetComponent<CocinaManager>();
        }

        public override TaskStatus OnUpdate()
        {
            if (pedido.Value != null)
                return TaskStatus.Success;

            if (pedidosParaEmpezar())
            {
                return TaskStatus.Success;
            }
            else if (pedidosParaAyudar())
            {
                return TaskStatus.Success;
            }
            else
                return TaskStatus.Failure;
        }

        private bool pedidosParaEmpezar()
        {
            if (caja.hayPedidosParaEmpezar())
            {
                pedido.Value = caja.pedidoPorEmpezar();
                cocina.empezarPedido(pedido.Value);
                return true;
            }
            else
                return false;
        }

        private bool pedidosParaAyudar()
        {
            if (cocina.hayPedidosHaciendose())
            {
                List<int> posibilidadesAyuda = new List<int>();
                posibilidadesAyuda.Add((int)MenuItem.Hamburguesa);
                posibilidadesAyuda.Add((int)MenuItem.Patatas);

                pedido.Value = cocina.pedidoEnElQueAyudar(posibilidadesAyuda);
                if (pedido.Value != null)
                {
                    for (int i = 0; i < posibilidadesAyuda.Count; i++)
                    {
                        if (!pedido.Value.GetComponent<Menu>().itemHecho((MenuItem)posibilidadesAyuda[i]))
                        {
                            pedido.Value.GetComponent<Menu>().empezarHacerItem((MenuItem)posibilidadesAyuda[i]);
                            break;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}