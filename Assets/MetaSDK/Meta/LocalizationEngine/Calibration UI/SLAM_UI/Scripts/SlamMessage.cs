using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// SLAM messages content and appearance
    /// </summary>
    public class SlamMessage
    {
        private string _title;
        private string _content;
        private Color? _titleColor;
        private Color? _contentColor;

        public SlamMessage(string title, string content, Color? titleColor = null, Color? contentColor = null)
        {
            _title = title;
            _content = content;
            _titleColor = titleColor;
            _contentColor = contentColor;
        }

        /// <summary>
        /// The title of the message
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// The content of the message
        /// </summary>
        public string Content
        {
            get { return _content; }
        }

        /// <summary>
        /// The color of the title
        /// </summary>
        public Color? TitleColor
        {
            get { return _titleColor; }
        }

        /// <summary>
        /// The color of the content
        /// </summary>
        public Color? ContentColor
        {
            get { return _contentColor; }
        }
    }
}