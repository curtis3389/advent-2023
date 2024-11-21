#!/usr/bin/ruby
def to_digit_char(s)
  case s
  when '1', 'one', 'eno'
    '1'
  when '2', 'two', 'owt'
    '2'
  when '3', 'three', 'eerht'
    '3'
  when '4', 'four', 'ruof'
    '4'
  when '5', 'five', 'evif'
    '5'
  when '6', 'six', 'xis'
    '6'
  when '7', 'seven', 'neves'
    '7'
  when '8', 'eight', 'thgie'
    '8'
  when '9', 'nine', 'enin'
    '9'
  when '0', 'zero', 'orez'
    '0'
  end
end

def to_calibration(line)
  first_regex = /(\d|one|two|three|four|five|six|seven|eight|nine|zero)/
  last_regex = /(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|orez)/
  first = to_digit_char(line.match(first_regex)[1])
  last = to_digit_char(line.reverse.match(last_regex)[1])
  (first + last).to_i
end

total =
  File
  .readlines('../inputs/day/1/input')
  .map(&:strip)
  .reject(&:empty?)
  .map { |line| to_calibration(line) }
  .sum

puts total
