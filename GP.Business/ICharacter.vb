Public Interface ICharacter
    ReadOnly Property Id As Integer
    Function GetStatistic(statisticType As String) As Integer
    Sub SetStatistic(statisticType As String, statisticValue As Integer)
    Sub AddItem(item As IItem)
    ReadOnly Property World As IWorld
End Interface
