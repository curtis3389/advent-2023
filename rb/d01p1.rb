#!/usr/bin/ruby
total =
  File
  .readlines('../inputs/day/1/input')
  .map(&:strip)
  .reject(&:empty?)
  .map do |line|
    numbers = line.chars.select { |c| c.match?(/\d/) }
    (numbers.first + numbers.last).to_i
  end
  .sum
puts total
