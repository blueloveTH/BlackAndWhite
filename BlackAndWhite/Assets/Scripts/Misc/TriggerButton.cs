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
    [SerializeField] AudioClip sfx;

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
        if (machine.currentNode.CompareName("1")) return;
        machine.Enter("1");
        SFX.Play(sfx);
        onActivate?.Invoke();
    }
}
