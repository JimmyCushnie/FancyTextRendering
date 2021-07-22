using UnityEngine;

namespace LogicUI.FancyTextRendering.MarkdownLogic
{
    class Bold : SimpleMarkdownTag
    {
        protected override string MarkdownIndicator => "**";
        protected override string RichTextOpenTag => "<b>";
        protected override string RichTextCloseTag => "</b>";

        protected override char? IgnoreContents => '*';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Bold.RenderBold;
    }

    class Italics : SimpleMarkdownTag
    {
        protected override string MarkdownIndicator => "*";
        protected override string RichTextOpenTag => "<i>";
        protected override string RichTextCloseTag => "</i>";

        protected override char? IgnoreContents => '*';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Italics.RenderItalics;
    }

    class Strikethrough : SimpleMarkdownTag
    {
        protected override string MarkdownIndicator => "~~";
        protected override string RichTextOpenTag => "<s>";
        protected override string RichTextCloseTag => "</s>";

        protected override char? IgnoreContents => '~';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Strikethrough.RenderStrikethrough;
    }

    class Monospace : MarkdownTag
    {
        protected override string GetMarkdownIndicator(MarkdownRenderingSettings _) => "`";

        // Optimization not critical, as this is only called once per markdown render
        protected override string GetRichTextOpenTag(MarkdownRenderingSettings settings)
        {
            string tag = string.Empty;

            if (settings.Monospace.AddSeparationSpacing)
                tag += $"<space={settings.Monospace.SeparationSpacing}em>";

            if (settings.Monospace.UseCustomFont)
                tag += $"<font=\"{settings.Monospace.FontAssetPathRelativeToResources}\">";

            if (settings.Monospace.DrawOverlay)
            {
                var padding = settings.Monospace.OverlayPaddingPixels;
                tag += $"<mark=#{ColorUtility.ToHtmlStringRGBA(settings.Monospace.OverlayColor)} padding=\"{padding},{padding},0,0\">";
            }

            if (settings.Monospace.ManuallySetCharacterSpacing)
                tag += $"<mspace={settings.Monospace.CharacterSpacing}em>";

            return tag;
        }

        protected override string GetRichTextCloseTag(MarkdownRenderingSettings settings)
        {
            string tag = string.Empty;

            if (settings.Monospace.UseCustomFont)
                tag += "</font>";

            if (settings.Monospace.DrawOverlay)
                tag += "</mark>";

            if (settings.Monospace.ManuallySetCharacterSpacing)
                tag += "</mspace>";

            if (settings.Monospace.AddSeparationSpacing)
                tag += $"<space={settings.Monospace.SeparationSpacing}em>";

            return tag;
        }


        protected override char? IgnoreContents => '`';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Monospace.RenderMonospace;
    }
    
    class Superscript : SimpleAsymmetricMarkdownTag
    {
        protected override string MarkdownIndicator => "^";
        protected override string RichTextOpenTag => "<sup>";
        protected override string RichTextCloseTag => "</sup>";

        protected override char? IgnoreContents => '^';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Superscript.RenderSuperscript;
    }
    
    class Subscript : SimpleAsymmetricMarkdownTag
    {
        protected override string MarkdownIndicator => "_";
        protected override string RichTextOpenTag => "<sub>";
        protected override string RichTextCloseTag => "</sub>";

        protected override char? IgnoreContents => '^';

        protected override bool AllowedToProces(MarkdownRenderingSettings settings)
            => settings.Subscript.RenderSubscript;
    }
}