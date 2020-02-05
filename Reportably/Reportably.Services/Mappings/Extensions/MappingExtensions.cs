            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (input is null || input.Count == 0)
            {
                return Array.Empty<TOut>();
