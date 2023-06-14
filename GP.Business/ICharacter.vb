Public Interface ICharacter
    ReadOnly Property Id As Integer
    Function GetStatistic(statisticType As String) As Integer
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
End Interface
