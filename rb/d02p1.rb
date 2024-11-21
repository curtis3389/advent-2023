#!/usr/bin/ruby
require_relative 'parser'

class Draw
  attr_accessor :red, :green, :blue

  def initialize(red, green, blue)
    @red = red
    @green = green
    @blue = blue
  end

  def self.parse_all(line)
    color = Parser.or(Parser.string('red'), Parser.string('green'), Parser.string('blue'))
    count = Parser.and(Parser.number, Parser.whitespace, color)
    count_sep = Parser.and(Parser.char(','), Parser.whitespace)
    draw = Parser.separate_by(count, count_sep)
    draw_sep = Parser.and(Parser.char(';'), Parser.whitespace)
    game = Parser.separate_by(draw, draw_sep)

    result = game.parse line

    result[0].select { |draw| draw[0] != ';' }.map { |draw|
      draw = draw.select { |count| count[0] != ',' }
      red = Draw.get_count draw, 'red'
      green = Draw.get_count draw, 'green'
      blue = Draw.get_count draw, 'blue'
      Draw.new red, green, blue
    }
  end

  def self.get_count(draw, color)
    count = draw.find { |count| count[2] == color }
    if count.nil?
      0
    else
      count[0].to_i
    end
  end
end

class Game
  attr_accessor :number, :draws

  def initialize(number, draws)
    @number = number
    @draws = draws
  end

  def self.parse(line)
    prefix = Parser.and(
      Parser.string('Game'),
      Parser.whitespace,
      Parser.number,
      Parser.char(':'),
      Parser.whitespace
    )

    result = prefix.parse line
    number = result[0][2].to_i
    draws = Draw.parse_all result[1]

    Game.new number, draws
  end

  def possible?
    @draws.all? { |draw| draw.red <= 12 && draw.green <= 13 && draw.blue <= 14 }
  end
end

puts File
  .readlines('../inputs/day/2/input')
  .map { |line| Game.parse line }
  .select(&:possible?)
  .map(&:number)
  .sum
