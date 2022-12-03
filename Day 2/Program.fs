// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

type HandShape = Rock | Paper | Scissor 
type OutCome = Win | Draw | Lose
type ParsedCode = 
    | HandShape of HandShape
    | OutCome of OutCome

let splitSpace (inp: string) = 
     inp.Split(" ", StringSplitOptions.None)

let parsePlaysPart1 str = 
    match str with
    |"A" | "X" -> Rock
    |"B" | "Y" -> Paper
    | _  -> Scissor // C Z

let parsePlaysPart2 str : ParsedCode = 
    match str with
    |"A" -> HandShape Rock
    |"B" -> HandShape Paper
    |"C" -> HandShape Scissor
    |"X" -> OutCome Lose 
    |"Y" -> OutCome Draw
    |_  -> OutCome Win // Z

let handShapeWinners = 
    [Rock, Paper; Scissor, Rock; Paper, Scissor]
    |> Map.ofList

let handShapeExtraScore = 
    [Rock, 1; Paper, 2; Scissor, 3]
    |> Map.ofList

let getShapeForOutcome (round:ParsedCode*ParsedCode) = 
    match round with
    | (HandShape hand, OutCome Draw) -> hand, hand
    | (HandShape hand, OutCome Win) ->  hand, handShapeWinners[hand]
    | (HandShape hand, OutCome Lose) -> hand, handShapeWinners[handShapeWinners[hand]]

let scorePlay round = 
    match round with
    | (opp, player) when opp = player -> 3 + handShapeExtraScore[player]
    | (Scissor, Rock) -> 6 + handShapeExtraScore[Rock]
    | (Rock, Paper) -> 6 + handShapeExtraScore[Paper]
    | (Paper, Scissor) -> 6 + handShapeExtraScore[Scissor]
    | _ ,player -> handShapeExtraScore[player]

[<EntryPoint>]
let main argv = 
    let input = 
        File.ReadAllLines(__SOURCE_DIRECTORY__ + "/input.txt")
        |> Array.map splitSpace
    let part1 = 
        input
        |> Array.map (Array.map parsePlaysPart1)
        |> Array.map (fun xs -> (xs.[0], xs.[1]))
        |> Array.map scorePlay
        |> Array.reduce (+)
    let part2 =
        input
        |> Array.map (Array.map parsePlaysPart2)
        |> Array.map (fun xs -> (xs.[0], xs.[1]))
        |> Array.map getShapeForOutcome
        |> Array.map scorePlay
        |> Array.reduce (+)
    printfn "Part1: %i" part1
    printfn "Part2: %i" part2
    0
