Imports System.Runtime.CompilerServices
Friend Module CharacterVerbs
    <Extension>
    Friend Sub DoVerb(character As ICharacter, verb As String, ParamArray parms As (String, Integer)())
        Dim table = parms.ToDictionary(Function(x) x.Item1, Function(x) x.Item2)
        Select Case verb
            Case VerbType.Move
                DoMove(character, table)
            Case VerbType.Eat
                DoEat(character, table)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Private Sub DoEat(character As ICharacter, table As Dictionary(Of String, Integer))
        character.SetSnax(character.Snax - 1)
        character.SetSatiety(character.Satiety + table(StatisticType.Satiety))
    End Sub
    Private Sub DoMove(character As ICharacter, table As Dictionary(Of String, Integer))
        character.SetDistanceRemaining(character.DistanceRemaining - table(StatisticType.Distance))
        ApplyHunger(character, table(StatisticType.Distance))
        RefreshLocalArea(character)
    End Sub
End Module
