Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        Dim gwen = world.CreateCharacter()
        gwen.SetStatistic(StatisticTypes.DistanceRemaining, InitialDistanceRemaining)
        world.Avatar = gwen
    End Sub
End Module
