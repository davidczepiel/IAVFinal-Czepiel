﻿namespace UCM.IAV.Movimiento
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CajaManager : MonoBehaviour
    {
        //Menu que se va a ofrecer
        public GameObject menuPrefab;

        private List<GameObject> pedidosParaCompletar = new List<GameObject>();
        private List<GameObject> pedidosParaRecoger = new List<GameObject>();
        private List<GameObject> pedidosParaEmpezar = new List<GameObject>();

        private List<bool> cajaAtendida = new List<bool>();
        private List<bool> clienteEnCaja = new List<bool>();
        private List<bool> cajaControlada = new List<bool>();

        public GameObject lugarCaja;

        private void Start()
        {
            cajaAtendida.Add(false);

            clienteEnCaja.Add(false);
            cajaControlada.Add(false);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public bool hayPedidosParaEmpezar()
        {
            return pedidosParaEmpezar.Count > 0;
        }

        public bool hayPedidosParaCompletar()
        {
            return pedidosParaCompletar.Count > 0;
        }

        public bool hayPedidosParaRecoger()
        {
            return pedidosParaRecoger.Count > 0;
        }

        public void añadirPedidoPorCompletar(GameObject pedido)
        {
            if (!pedidosParaCompletar.Contains(pedido))
                pedidosParaCompletar.Add(pedido);
        }

        public void añadirPedidoPorRegoger(GameObject pedido)
        {
            if (!pedidosParaRecoger.Contains(pedido))
                pedidosParaRecoger.Add(pedido);
        }

        public void eliminarPedidoPorCompletar(GameObject pedido)
        {
            if (pedidosParaCompletar.Contains(pedido))
                pedidosParaCompletar.Remove(pedido);
        }

        public GameObject pedidoPorEmpezar()
        {
            GameObject nuevo = pedidosParaEmpezar[0];
            pedidosParaEmpezar.RemoveAt(0);
            return nuevo;
        }

        public GameObject pedidoPorCompletar()
        {
            GameObject nuevo = pedidosParaCompletar[0];
            pedidosParaCompletar.RemoveAt(0);
            return nuevo;
        }

        public GameObject pedidoPorEntregar()
        {
            GameObject nuevo = pedidosParaRecoger[0];
            pedidosParaRecoger.RemoveAt(0);
            return nuevo;
        }

        public bool hayClientesParaPedir()
        {
            int i = 0;
            while (i < clienteEnCaja.Count && (!clienteEnCaja[i] || (clienteEnCaja[i] && cajaControlada[i])))
                i++;
            return i < clienteEnCaja.Count;
        }

        public int dameCajaAtender()
        {
            int i = 0;
            while (i < clienteEnCaja.Count && (!clienteEnCaja[i] || (clienteEnCaja[i] && cajaControlada[i])))
                i++;
            cajaControlada[i] = true;
            return i;
        }

        public void atenderCliente()
        {
            int i = 0;
            while (i < clienteEnCaja.Count && (!clienteEnCaja[i] || (clienteEnCaja[i] && cajaAtendida[i])))
                i++;

            cajaAtendida[i] = true;
        }

        public void atenderCliente(int numCaja)
        {
            cajaAtendida[numCaja] = true;
        }

        public int darCajaCliente()
        {
            int i = 0;
            //while (!clienteEnCaja[i]) i++;
            clienteEnCaja[0] = true;
            return 0;
        }

        public bool meHanAtendido(int numCaja)
        {
            return cajaAtendida[numCaja];
        }

        public void hacerPedido(int numCaja, GameObject pedidoNuevo)
        {
            pedidosParaEmpezar.Add(pedidoNuevo);
            clienteEnCaja[numCaja] = false;
            cajaAtendida[numCaja] = false;
            cajaControlada[numCaja] = false;
        }

        public GameObject pedidoEnElQueAyudar(List<int> posiblesElementos)
        {
            GameObject pedido = null;
            int i = 0;
            bool bucle = true;
            while (i < pedidosParaCompletar.Count && bucle)
            {
                GameObject actual = pedidosParaCompletar[i];
                Menu menu = actual.GetComponent<Menu>();
                for (int j = 0; j < posiblesElementos.Count; j++)
                {
                    if (!menu.itemHecho((MenuItem)posiblesElementos[j]))
                    {
                        bucle = false;
                        pedido = actual;
                        break;
                    }
                }
                i++;
            }
            return pedido;
        }
    }
}