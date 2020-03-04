﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Profile.Header.Components;
using osuTK;

namespace osu.Game.Users
{
    public class UserGridPanel : UserPanel
    {
        private const int margin = 10;

        public UserGridPanel(User user)
            : base(user)
        {
            Height = 120;
            CornerRadius = 10;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Background.FadeTo(0.4f);
        }

        protected override Drawable CreateLayout()
        {
            FillFlowContainer details;

            var layout = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(margin),
                Child = new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension()
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension()
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            CreateAvatar().With(avatar =>
                            {
                                avatar.Size = new Vector2(60);
                                avatar.Margin = new MarginPadding { Bottom = margin };
                                avatar.Masking = true;
                                avatar.CornerRadius = 6;
                            }),
                            new FillFlowContainer
                            {
                                AutoSizeAxes = Axes.Both,
                                Direction = FillDirection.Vertical,
                                Spacing = new Vector2(0, 7),
                                Margin = new MarginPadding { Left = margin },
                                Children = new Drawable[]
                                {
                                    details = new FillFlowContainer
                                    {
                                        AutoSizeAxes = Axes.Both,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(6),
                                        Children = new Drawable[]
                                        {
                                            CreateFlag(),
                                        }
                                    },
                                    CreateUsername(),
                                }
                            }
                        },
                        new Drawable[]
                        {
                            CreateStatusIcon().With(icon =>
                            {
                                icon.Anchor = Anchor.Centre;
                                icon.Origin = Anchor.Centre;
                            }),
                            CreateStatusMessage(false).With(message =>
                            {
                                message.Anchor = Anchor.CentreLeft;
                                message.Origin = Anchor.CentreLeft;
                                message.Margin = new MarginPadding { Left = margin };
                            })
                        }
                    }
                }
            };

            if (User.IsSupporter)
            {
                details.Add(new SupporterIcon
                {
                    Height = 26,
                    SupportLevel = User.SupportLevel
                });
            }

            return layout;
        }
    }
}
