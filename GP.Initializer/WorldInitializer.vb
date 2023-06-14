Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        Dim gwen = world.CreateCharacter()
        With gwen
            .SetStatistic(StatisticTypes.DistanceRemaining, InitialDistanceRemaining)
            .SetStatistic(StatisticTypes.MaximumSatiety, Constants.MaximumSatiety)
            .SetStatistic(StatisticTypes.Satiety, Constants.MaximumSatiety)
            .SetStatistic(StatisticTypes.MaximumHealth, Constants.MaximumHealth)
            .SetStatistic(StatisticTypes.Health, Constants.MaximumHealth)
        End With
        world.Avatar = gwen
    End Sub
End Module
