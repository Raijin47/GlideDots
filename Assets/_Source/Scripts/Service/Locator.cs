using System;
using UnityEngine;

[Serializable]
public class Locator
{
    [SerializeField] private LilyHandler _lilyHandler;
    [SerializeField] private PanelCreateLily _panelLily;
    [SerializeField] private BallFactory _factory;
    [SerializeField] private PlayerBase _player;

    [SerializeField] private TutorialHelper _tutorialHelper;
    [SerializeField] private Tutorial _tutorial;

    public LilyHandler LilyHandler => _lilyHandler;
    public PanelCreateLily PanelLily => _panelLily;
    public BallFactory Factory => _factory;
    public PlayerBase Player => _player;

    public Tutorial Tutorial => _tutorial;
    public TutorialHelper Hand3D => _tutorialHelper;
}