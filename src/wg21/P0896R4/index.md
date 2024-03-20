# P0896R4: The One Ranges Proposal

[P0896R4](https://wg21.link/P0896R4) is a proposal for C++20 that introduces a new header `<ranges>` and a set of utilities and concepts related to range-based programming. The main goal of this proposal is to provide a unified and consistent approach to working with ranges, which are sequences of elements that can be iterated over.

```cpp
#include <ranges>
#include <vector>

const std::vector<std::uint64_t> even_square =
    std::views::iota(static_cast<std::uint64_t>(1)) | std::views::take(10) |
    std::views::filter([](const std::uint64_t element) static noexcept -> bool {
      return element % 2 == 0;
    }) |
    std::views::transform(
        [](const std::uint64_t element) static noexcept -> std::uint64_t {
          return element * element;
        }
    ) |
    std::ranges::to<std::vector<std::uint64_t>>();
```
