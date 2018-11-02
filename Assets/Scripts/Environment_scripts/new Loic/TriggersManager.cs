using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{

    public TriggerDecort[] a;
    public List<TriggerDecortBackgroundBackAndForth> b = new List<TriggerDecortBackgroundBackAndForth>();

    public List<TriggerDecortBackgroundMove> c = new List<TriggerDecortBackgroundMove>();
    public List<TriggerDecortBackgroundRotate> d = new List<TriggerDecortBackgroundRotate>();
    public List<TriggerDecortBackgroundScale> e = new List<TriggerDecortBackgroundScale>();
    private Transform player;



    //private List<TriggerDecortBackgroundBackAndForth> b1 = new List<TriggerDecortBackgroundBackAndForth>();
    private List<TriggerDecortBackgroundMove> c1;
    private List<TriggerDecortBackgroundRotate> d1;
    private List<TriggerDecortBackgroundScale> e1;

    private float posPlayer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RestartList();
    }

    public void RestartList()
    {
        c1 = new List<TriggerDecortBackgroundMove>(c);
        d1 = new List<TriggerDecortBackgroundRotate>(d);
        e1 = new List<TriggerDecortBackgroundScale>(e);
        TriListC();
        TriListD();
        TriListE();
    }

    void Update()
    {
        posPlayer = player.position.z;
        RestartCF();
        RestartDF();
        RestartEF();
    }

    public void BuildLevel()
    {
        RestartList();
        RestartA();
        RestartC();
        RestartD();
        RestartE();
    }

    public void TriListC()
    {
        bool Tryokc = false;

        while (!Tryokc)
        {
            Tryokc = true;
            for (int i = 0; i < c1.Count - 1; i++)
            {
                if (c1[i].FirstposZ > c1[i + 1].FirstposZ)
                {
                    TriggerDecortBackgroundMove c2 = c1[i + 1];
                    c1[i + 1] = c1[i];
                    c1[i] = c2;
                    Tryokc = false;
                }
            }
        }
    }

    public void TriListD()
    {
        bool Tryokd = false;

        while (!Tryokd)
        {
            Tryokd = true;
            for (int i = 0; i < d1.Count - 1; i++)
            {
                if (d1[i].FirstposZ > d1[i + 1].FirstposZ)
                {
                    TriggerDecortBackgroundRotate d2 = d1[i + 1];
                    d1[i + 1] = d1[i];
                    d1[i] = d2;
                    Tryokd = false;
                }
            }
        }
    }

    public void TriListE()
    {
        bool Tryoke = false;

        while (!Tryoke)
        {
            Tryoke = true;
            for (int i = 0; i < e1.Count - 1; i++)
            {
                if (e1[i].FirstposZ > e1[i + 1].FirstposZ)
                {
                    TriggerDecortBackgroundScale e2 = e1[i + 1];
                    e1[i + 1] = e1[i];
                    e1[i] = e2;
                    Tryoke = false;
                }
            }
        }
    }
    void RestartCF()
    {
        if (c1.Count != 0)
        {
            if (posPlayer > c1[0].FirstposZ)
            {
                for (int i = 0; i < c1.Count; i++)
                {
                    if (posPlayer > c1[i].FirstposZ)
                    {
                        c1[i].enabled = true;
                        c1.Remove(c1[i]);
                        i--;
                    }
                }
            }
        }
    }

    void RestartDF()
    {
        if (d1.Count != 0)
        {
            if (posPlayer > d1[0].FirstposZ)
            {
                for (int i = 0; i < d1.Count; i++)
                {
                    if (posPlayer > d1[i].FirstposZ)
                    {
                        d1[i].enabled = true;
                        d1.Remove(d1[i]);
                        i--;
                    }
                }
            }
        }
    }

    void RestartEF()
    {
        if (e1.Count != 0)
        {
            if (posPlayer > e1[0].FirstposZ)
            {
                for (int i = 0; i < e1.Count; i++)
                {
                    if (posPlayer > e1[i].FirstposZ)
                    {
                        e1[i].enabled = true;
                        e1.Remove(e1[i]);
                        i--;
                    }
                }
            }
        }
    }

    void RestartA()
    {
        float playerZposition = player.position.z;
        for (int i = 0; i < a.Length; i++)
        {
            if (playerZposition < a[i].gameObject.transform.position.z)
            {
                a[i].DesactivateScript();
            }
            else
            {
                return;
            }
        }
    }

    void RestartB()
    {
        float playerZposition = player.position.z;
        for (int i = 0; i < b.Count; i++)
        {
            if (playerZposition < b[i].FirstposZ)
            {
                b[i].DesactivateScript();
            }
        }
    }

    void RestartC()
    {
        float playerZposition = player.position.z;
        for (int i = 0; i < c1.Count; i++)
        {
            if (playerZposition < c1[i].FirstposZ)
            {
                c1[i].DesactivateScript();
            }
            else
            {
                c1.Remove(c1[i]);
                i--;
            }
        }
    }

    void RestartD()
    {
        float playerZposition = player.position.z;
        for (int i = 0; i < d1.Count; i++)
        {
            if (playerZposition < d1[i].FirstposZ)
            {
                d1[i].DesactivateScript();
            }
            else
            {
                d1.Remove(d1[i]);
                i--;
            }
        }
    }

    void RestartE()
    {
        float playerZposition = player.position.z;
        for (int i = 0; i < e1.Count; i++)
        {
            if (playerZposition < e1[i].FirstposZ)
            {
                e1[i].DesactivateScript();
            }
            else
            {
                e1.Remove(e1[i]);
                i--;
            }
        }
    }
}