﻿namespace DatabaseConverter.Model
{
    public class TranslateResult
    {
        public object Error { get; set; }
        public object Data { get; set; }

        public bool HasError => this.Error != null;
    }
}
