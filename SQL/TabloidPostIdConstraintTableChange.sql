Alter Table PostReaction
Drop Constraint FK_PostReaction_Post;
Alter Table PostReaction
Add Constraint FK_PostReaction_Post
Foreign Key (PostId) References Post (Id)
on Delete Cascade;

Alter Table PostTag
Drop Constraint FK_PostTag_Post;
Alter Table PostTag
Add Constraint FK_PostTag_Post
Foreign Key (PostId) References Post (Id)
on Delete Cascade;

Alter Table Comment
Drop Constraint FK_Comment_Post;
Alter Table Comment
Add Constraint FK_Comment_Post
Foreign Key (PostId) References Post (Id)
on Delete Cascade;