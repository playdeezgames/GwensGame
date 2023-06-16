Imports System.Runtime.CompilerServices
Friend Module CharacterVerbs
    Private ReadOnly verbTable As IReadOnlyDictionary(Of String, Action(Of ICharacter, IReadOnlyDictionary(Of String, Integer))) =
        New Dictionary(Of String, Action(Of ICharacter, IReadOnlyDictionary(Of String, Integer))) From
        {
            {VerbType.Move, AddressOf DoMove},
            {VerbType.Eat, AddressOf DoEat},
            {VerbType.Forage, AddressOf DoForage}
        }
    <Extension>
    Friend Sub DoVerb(character As ICharacter, verb As String, ParamArray parms As (String, Integer)())
        verbTable(verb)(character, parms.ToDictionary(Function(x) x.Item1, Function(x) x.Item2))
    End Sub
    Private Sub DoForage(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer))
        Throw New NotImplementedException()
    End Sub
    Private Sub DoEat(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer))
        character.SetSnax(character.Snax - 1)
        character.SetSatiety(character.Satiety + table(StatisticType.Satiety))
    End Sub
    Private Sub DoMove(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer))
        character.SetDistanceRemaining(character.DistanceRemaining - table(StatisticType.Distance))
        ApplyHunger(character, table(StatisticType.Distance))
        RefreshLocalArea(character)
    End Sub
End Module
