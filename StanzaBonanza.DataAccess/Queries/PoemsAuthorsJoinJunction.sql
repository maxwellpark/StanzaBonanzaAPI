select poems.*, Authors.* from poems 
inner join Poems_Authors on Poems.PoemId = Poems_Authors.PoemId 
inner join authors on Authors.AuthorId = Poems_Authors.AuthorId;