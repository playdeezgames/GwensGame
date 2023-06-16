Imports System.Runtime.CompilerServices
Imports SPLORR.Game

Friend Module CharacterVerbs
    Private ReadOnly verbTable As IReadOnlyDictionary(Of String, Func(Of ICharacter, IReadOnlyDictionary(Of String, Integer), Boolean)) =
        New Dictionary(Of String, Func(Of ICharacter, IReadOnlyDictionary(Of String, Integer), Boolean)) From
        {
            {VerbType.Move, AddressOf DoMove},
            {VerbType.Eat, AddressOf DoEat},
            {VerbType.Forage, AddressOf DoForage},
            {VerbType.TakeShortcut, AddressOf DoTakeShortcut}
        }

    Private Function DoTakeShortcut(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer)) As Boolean
        If Not character.HasShortcut Then
            Return False
        End If
        character.SetDistanceRemaining(character.DistanceRemaining + character.Shortcut)
        character.ApplyHunger(1)
        RefreshLocalArea(character)
        Return True
    End Function

    <Extension>
    Friend Function DoVerb(character As ICharacter, verb As String, ParamArray parms As (String, Integer)()) As Boolean
        Return verbTable(verb)(character, parms.ToDictionary(Function(x) x.Item1, Function(x) x.Item2))
    End Function
    Private Function DoForage(character As ICharacter, table As IReadOnlyDictionary(Of String, Integer)) As Boolean
        If Not character.CanForage Then
            Return False
        End If
        character.ApplyHunger(1)
        Dim roll = RNG.RollDice("1d100") + character.ForagingDifficulty
        If roll <= character.ForagingSkill Then
            character.SetForagingAbundance(character.ForagingAbundance - 1)
            character.SetSnax(character.Snax + 1)
            Return True
        End If
        Return False
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
