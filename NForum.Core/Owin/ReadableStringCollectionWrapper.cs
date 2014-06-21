//using Microsoft.Owin;
//using NForum.Core.Abstractions;
//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace NForum.Core.Owin {

//	internal class ReadableStringCollectionWrapper : INameValueCollection {
//		private readonly IReadableStringCollection readableStringCollection;

//		public ReadableStringCollectionWrapper(IReadableStringCollection readableStringCollection) {
//			this.readableStringCollection = readableStringCollection;
//		}

//		public String this[String key] {
//			get {
//				return this.readableStringCollection[key];
//			}
//		}

//		public IEnumerable<String> GetValues(String key) {
//			return this.readableStringCollection.GetValues(key);
//		}

//		public String Get(String key) {
//			return this.readableStringCollection.Get(key);
//		}

//		public IEnumerator<KeyValuePair<String, String>> GetEnumerator() {
//			return GetEnumerable().GetEnumerator();
//		}

//		IEnumerator IEnumerable.GetEnumerator() {
//			return GetEnumerator();
//		}

//		private IEnumerable<KeyValuePair<String, String>> GetEnumerable() {
//			foreach (KeyValuePair<String, String[]> pair in this.readableStringCollection) {
//				yield return new KeyValuePair<String, String>(pair.Key, this.readableStringCollection.Get(pair.Key));
//			}
//		}
//	}
//}