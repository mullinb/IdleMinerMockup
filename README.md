  <h1><a href="http://idleminermockup.herokuapp.com/">
IdleMinerMockup</a>
</h1>

#### This is demo mock up of a hugely popular android/iOS mobile game, made as an exercise in the Unity Editor.
<div align="center">
<img src="https://s3.amazonaws.com/fluxlymoppings/pics/SmallerGifOfGameplay26.03.gif" maxwidth="800">
</div>
As much behavior is shared, the code was written from the ground up to make use of C#'s very elegant inheritance model. Using some key principles from the first few chapters of <i>Clean Code</i> by Robert C. Martin, I focused on naming all methods and parameters as descriptively and consistently as possible, and keeping functions concise and limited to one level of abstraction. Functions are organized in the various behavioral classes as chronologically as they can be (in terms of typical behavior loops), with utility functions appearing towards the bottom.

## Unit Types

As the three main unit types, Transporter, Miner, and the Elevator, all function in very similar ways but with some key differences, inherited classes from the generalized "WorkerBehavior" class were used to define their behavior. No GameObjects have "WorkerBehavior" attached, but the majority of each unit's functionality is nonetheless defined therein. Some of the override methods in the inherited classes got a little ugly, as they were redefining or adding to major Coroutine functionality. The Coroutines govern the asset transfer to the various Buckets, with each worker type behaving slightly differently, including many requiring animation (a separate Coroutine call in my code). Chained Coroutines are somewhat unwieldy to read but they do work, and they allow for the kind of separation of concerns (Do One Thing) emphasized in <i>Clean Code</i>. I would be like to know how this formulation could be improved upon.

## Bucket Behavior

Bucket Behavior is a simple script that runs for the elevator Bucket and all mine Buckets. The Buckets are referred to as Deposits in game, which I will refactor. A very slightly modified form runs for the Transporter Bucket, as the Transporter Bucket is actually the PlayerBank. This Bank is different as it needs be globally accessible to the player via UI elements, allowing for expenditures. The Game Manager directly manages one type of such expenditure, namely, the purchasing of new Mine Shafts as the player progresses, and maintains a numerical reference to this element of the game state.

### Player Bank And Leveling Up

The other major interaction with the PlayerBank, the LevelingBehavior, was separated out from the Game Manager. This was done for three reasons: 

1. The Game Manager is already quite large.
2. The LevelingBehavior's functionality maps in a parallel fashion to the WorkerBehavior class, ie. Transporters, Miners, and Elevators, all level up in similar but ultimately divergent ways.
3. In at least one case (the MineLevelingBehavior), the behavior must be iterable and localized. That is, with every new Mine the Game Manager spawns, a separate LevelingBehavior specific to that mine is required.

## Game State

In sum, it is easy to suggest that the Game Manager shares game state control with the various LevelingBehavior subclasses; for instance, if you wanted to save the game state for a player who would return later, you would have to save all of the individual LevelingBehavior data (level, load speed, number of workers etc.) <i>as well</i>. In practice, though, breaking up this functionality across different scripts is effective because the various elements of the game state are functionally independent. The Game Manager doesn't care what level a Mine is because the causal loop of asset transfer is tight enough that it doesn't need to care. The above concerns thus override worries about saving game state or otherwise centralizing game state information.

## Try it out on Heroku

IdleMinerMockup can be played <a href="http://idleminermockup.herokuapp.com/">here on Heroku</a>. I recommend playing in full screen mode. Note that not all functionality from the original is represented here, only the core gameplay loop. Difficulty and progression balance are rough, simplified approximations of the original.
