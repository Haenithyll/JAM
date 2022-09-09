
using UnityEngine;

/// <summary>
/// Structure de données pour les instances de cartes dans le deck.<br/>
/// Placeholder par Nathan pour qu'il puisse faire les UI qui affiche des cartes, à remplacer par la version complète.
/// </summary>
public class CardData
{
    public Color Color { get => _randomColor; }
    
    private Color _randomColor;
    public CardData()
    {
        _randomColor = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
    }
}
