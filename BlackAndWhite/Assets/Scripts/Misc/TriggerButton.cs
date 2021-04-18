using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.Events;

public class TriggerButton : InteractiveBehaviour
{
    private FlowMachine machine;
    [SerializeField] GameObject disabledGo;
    [SerializeField] GameObject enabledGo;

    [SerializeField] UnityEvent onActivate;

    private void Awake()
    {
        machine = FlowMachine.BinaryDiagram();
        machine["0"].onEnter += (_) => disabledGo.SetActive(true);
        machine["0"].onExit += (_) => disabledGo.SetActive(false);
        machine["1"].onEnter += (_) => enabledGo.SetActive(true);
        machine["1"].onExit += (_) => enabledGo.SetActive(false);
        machine.Enter("0");
    }

    [SlotMethod("player_hit")]
    void OnSignal(Signal sig)
    {
        machine.Enter("1");
        onActivate?.Invoke();
    }
}
