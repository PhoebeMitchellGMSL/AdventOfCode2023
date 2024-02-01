module Tests

open Xunit
open Program
open FluentAssertions

[<Fact>]
let ``Sample`` () =
    let input = "1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet"
    let output = Day1 input
    output.Should().Be(142, "because that is the expected output")