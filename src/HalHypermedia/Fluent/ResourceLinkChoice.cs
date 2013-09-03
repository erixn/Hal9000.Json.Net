﻿using System;
using System.Collections.Generic;

namespace Hal9000.Json.Net.Fluent {
    public class ResourceLinkChoice : IResourceLinkChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;
        private readonly bool _predicate;

        internal ResourceLinkChoice ( FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                      HalRelation embeddedRelation, HalRelation linkRelation, bool predicate ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( embeddedResourceBuilder == null ) {
                throw new ArgumentNullException( "embeddedResourceBuilder" );
            }
            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            if ( linkRelation == null ) {
                throw new ArgumentNullException( "linkRelation" );
            }

            _builder = builder;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _embeddedRelation = embeddedRelation;
            _linkRelation = linkRelation;
            _predicate = predicate;
        }

        public IEmbeddedJoiner Link ( HalLink link ) {
            if ( link == null ) {
                throw new ArgumentNullException( "link" );
            }

            if ( _predicate ) {
                _embeddedResourceBuilder.IncludeRelationWithSingleLink( _linkRelation, link );
            }

            return new EmbeddedJoiner( _builder, _embeddedResourceBuilder, _embeddedRelation, _predicate );
        }

        public IEmbeddedJoiner Links ( IEnumerable<HalLink> links ) {
            if ( links == null ) {
                throw new ArgumentNullException( "links" );
            }

            if ( _predicate ) {
                _embeddedResourceBuilder.IncludeRelationWithMultipleLinks( _linkRelation, links );
            }
            return new EmbeddedJoiner( _builder, _embeddedResourceBuilder, _embeddedRelation, _predicate );
        }
    }
}