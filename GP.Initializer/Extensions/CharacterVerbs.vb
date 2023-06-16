Imports System.Runtime.CompilerServices
Friend Module CharacterVerbs
    Private ReadOnly verbTable As IReadOnlyDictionary(Of String, Func(Of ICharacter, IReadOnlyDictionary(Of String, Integer), Boolean)) =
        New Dictionary(Of String, Func(Of ICharacter, IReadOnlyDictionary(Of String, Integer), Boolean)) From
        {
            {VerbType.Move, AddressOf DoMove},
            {VerbType.Eat, AddressOf DoEat},
            {VerbType.Forage, AddressOf DoForage}
        }
    <Extension>
    Friend Function DoVerb(character As ICharacter, verb As String, ParamArray parms As (String, Integer)()) As Boolean
        Return verbTable(verb)(character, parms.ToDictionary(Function(x) x.Item1, Function(x) x.Item2))
    End Function
    Private Function DoForage(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer)) As Boolean
        Throw New NotImplementedException()
    End Function
    Private Function DoEat(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer)) As Boolean
        character.SetSnax(character.Snax - 1)
        character.SetSatiety(character.Satiety + table(StatisticType.Satiety))
        Return True
    End Function
    Private Function DoMove(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer)) As Boolean
        character.SetDistanceRemaining(character.DistanceRemaining - table(StatisticType.Distance))
        ApplyHunger(character, table(StatisticType.Distance))
        RefreshLocalArea(character)
        Return True
    End Function
End Module
