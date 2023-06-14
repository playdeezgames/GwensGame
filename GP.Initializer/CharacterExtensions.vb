Imports System.Runtime.CompilerServices

Public Module CharacterExtensions
    <Extension>
    Public Function DistanceRemaining(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.DistanceRemaining)
    End Function
    <Extension>
    Public Sub SetDistanceRemaining(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.DistanceRemaining, Math.Max(value, 0))
    End Sub
End Module
