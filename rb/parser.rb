class Parser
  def initialize(&parse)
    @parse = parse
  end

  def parse(input)
    @parse.call input
  end

  def self.any_char
    Parser.new do |input|
      unless input[0].nil?
        [input[0], input[1..]]
      else
        nil
      end
    end
  end

  def self.char(c)
    Parser.new do |input|
      if input.start_with? c
        [c, input[1..]]
      else
        nil
      end
    end
  end

  def self.digit
    Parser.new do |input|
      if input.start_with?(/\d/)
        [input[0], input[1..]]
      else
        nil
      end
    end
  end

  def self.kleene_plus(parser)
    Parser.new do |input|
      result = parser.parse(input)
      return nil if result.nil?

      results = result[0]
      remaining = result[1]

      loop do
        result = parser.parse(remaining)
        break if result.nil?

        results << result[0]
        remaining = result[1]
      end

      [results, remaining]
    end
  end

  def self.kleene_star(parser)
    Parser.new do |input|
      results = []
      remaining = input

      loop do
        result = parser.parse(remaining)
        break if result.nil?

        results << result[0]
        remaining = result[1]
      end

      [results, remaining]
    end
  end

  def self.number
    Parser.new do |input|
      match = input.match(/(\d+)/)
      if match.nil?
        nil
      else
        [match[1], input[match[1].length..]]
      end
    end
  end

  def self.separate_by(parser, separator)
    Parser.new do |input|
      flip = false
      results = []
      remaining = input

      loop do
        result = if !flip
                   parser.parse(remaining)
                 else
                   separator.parse(remaining)
                 end
        flip = !flip
        break if result.nil?

        results << result[0]
        remaining = result[1]
      end

      [results, remaining]
    end
  end

  def self.string(s)
    Parser.new do |input|
      if input.start_with? s
        [s, input[s.length..]]
      else
        nil
      end
    end
  end

  def self.whitespace
    Parser.new do |input|
      match = input.match(/(\s+)/)
      if match.nil?
        nil
      else
        [match[1], input[match[1].length..]]
      end
    end
  end

  def self.and(*parsers)
    Parser.new do |input|
      parsers.reduce([[], input]) {|a, n|
        if a.nil?
          nil
        else
          result = n.parse(a[1])
          if result.nil?
            nil
          else
            [a[0] << result[0], result[1]]
          end
        end
      }
    end
  end

  def self.or(*parsers)
    Parser.new do |input|
      parsers.reduce([nil, input]) {|a,n|
        if !a.nil? && !a[0].nil?
          a
        else
          result = n.parse(a[1])
          if result.nil?
            a
          else
            result
          end
        end
      }
    end
  end
end
