Friend Module GwenInitializer
    Friend Function Initialize(world As IWorld) As ICharacter
        Dim gwen = world.CreateCharacter()
        With gwen
            .SetStatistic(StatisticTypes.MaximumSatiety, Constants.MaximumSatiety)
            .SetStatistic(StatisticTypes.MaximumHealth, Constants.MaximumHealth)
            .SetDistanceRemaining(InitialDistanceRemaining)
            .SetSatiety(Constants.MaximumSatiety)
            .SetHealth(Constants.MaximumHealth)
            .SetPace(InitialPace)
            .SetSnax(InitialSnax)
            .SetForagingSkill(InitialForagingSkill)
        End With
        Return gwen
    End Function
End Module
