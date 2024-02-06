module Day2

open System.IO
open System.Text.RegularExpressions

let maxRed = 12
let maxGreen = 13
let maxBlue = 14

let RemoveGame(game : string) =
    Regex.Replace(game, @"Game \d+: ", "")

let MapGames(games : string array) : (int * string) array =
    let a = games
            |> Array.map _.Trim() 
            |> Array.map _.Split(" ")
            |> Array.map (fun game -> (game[0] |> int, game[1]))
    a

let IsGameValid game : bool =
    let count, colour = game
    match colour with
    | s when s = "red" -> count <= maxRed
    | s when s = "blue" -> count <= maxBlue
    | s when s = "green" -> count <= maxGreen
    | _ -> failwith "Invalid colour"

let Split(text : string) =
    text.Split [| ';'; ',' |]

let ProcessLine(text : string) =
    RemoveGame text
    |> Split
    |> MapGames
    |> Array.map IsGameValid
    |> Array.forall id

let Filter (game : int * bool) =
    match game with
    | i, v when v -> i + 1
    | _, v when not v -> 0
    | _, _ -> failwith "Invalid value"
    
let Day2(input : string) : int =
    input.Split "\n"
    |> Array.map ProcessLine
    |> Array.indexed
    |> Array.map Filter
    |> Array.sum
    
let input = File.ReadAllText("input.txt");
printfn $"{Day2(input)}"