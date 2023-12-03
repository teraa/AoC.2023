#!/bin/bash
set -e

# Creates new projects and adds them to Puzzles solution folder
# Downloads input file
# Adds to git index

add() {
  echo "Adding $prefix"

  for part in {a,b}; do
    project=$prefix.$part
    dotnet new aoc2023 -n $project
    dotnet sln add $project -s Puzzles
  done

  download

  git add $prefix.{a,b} *.sln
}

download() {
  # .headers file format
  # Cookie: <cookie>
  # user-agent: <user agent>

  echo "Downloading $prefix input"

  mkdir -p $prefix.{a,b}

  curl "https://adventofcode.com/$year/day/$day/input" -H @.headers -sSL \
  | tee $prefix.{a,b}/input.txt 1> /dev/null
}

remove() {
  echo "Removing $prefix"

  dotnet sln remove $prefix.{a,b}
  rm -r $prefix.{a,b}
  git add $prefix.{a,b} *.sln
}

set_day() {
  day=$OPTARG
  printf -v prefix "Day%02d" $day
}

: ${year:=2023}

while getopts "a:r:d:y:" arg; do
  case $arg in
    a) set_day; add;;
    r) set_day; remove;;
    d) set_day; download;;
    y) year=$OPTARG;;
  esac
done
