#!/bin/bash
set -e

# Creates new projects and adds them to Puzzles solution folder
# Downloads input file
# Adds to git index

add() {
  set_vars
  echo "Adding $prefix"

  for part in {a,b}; do
    project=$prefix.$part
    dotnet new aoc2023 -n $project
    dotnet sln add $project -s Puzzles
  done

  # .headers file format
  # Cookie: <cookie>
  # user-agent: <user agent>

  curl "https://adventofcode.com/$year/day/$day/input" -H @.headers -sSL \
  | tee $prefix.{a,b}/input.txt 1> /dev/null

  git add $prefix.{a,b} *.sln
}

remove() {
  set_vars
  echo "Removing $prefix"

  dotnet sln remove $prefix.{a,b}
  rm -r $prefix.{a,b}
  git add $prefix.{a,b} *.sln
}

set_vars() {
  day=$OPTARG
  printf -v prefix "Day%02d" $day
}

: ${year:=2023}

while getopts "a:r:y:" arg; do
  case $arg in
    a) add;;
    r) remove;;
    y) year=$OPTARG;;
  esac
done
